using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Storage;

namespace RescueAnimalsUniversal
{
    [XmlRoot("root")]
    public class UserController
    {
        [XmlArray("ListUser")]
        [XmlArrayItem("User")]
        public List<User> ListUser = new List<User>();

        public async Task LoadDataAsync()
        {
            //kiểm tra file đã có chưa, nếu chưa thì tạo file mới

            var list = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            StorageFile file = null;
            foreach (var item in list)
            {
                if (item.Name.Equals("users.xml"))
                    file = item;
            }

            if (file == null)
                file = await CreateDefaultFile();


            String content = await Helper.ReadFileApplicationDatAsync("users.xml");
            XDocument xDoc = XDocument.Parse(content);
            XmlSerializer se = new XmlSerializer(typeof(UserController));
            this.ListUser = (se.Deserialize(xDoc.CreateReader()) as UserController).ListUser;
        }

        public async Task AddNewUserAsync(User u)
        {
            ListUser.Add(u);
            List<String> content = GenarateFileStringContent();
            await Helper.WriteToFileApplicationDataAsync(content.ToArray(), "users.xml");
        }

        public async Task SaveDataAsync()
        {
            List<String> content = GenarateFileStringContent();
            await Helper.WriteToFileApplicationDataAsync(content.ToArray(), "users.xml");
        }

        public User SelectUser(string name)
        {
            foreach (User u in ListUser)
            {
                if (u.Name.Equals(name))
                    return u;
            }
            return null;
        }


        //CHỗ này cần sửa khi thay đổi số cửa
        private List<string> GenarateFileStringContent()
        {
            List<string> content = new List<string>();
            content.Add("<root>");
            content.Add("<ListUser>");
            foreach (User u in ListUser)
            {
                content.Add("<User>");
                content.Add("<Name>" + u.Name + "</Name>");
                content.Add("<LinkAvatar>" + u.LinkAvatar + "</LinkAvatar>");

                content.Add("<ListPoint>");
                foreach (int i in u.ListPoint)
                    content.Add("<Point>" + i + "</Point>");
                content.Add("</ListPoint>");


                content.Add("</User>");
            }
            content.Add("</ListUser>");
            content.Add("</root>");
            return content;
        }

        public List<User> ListUserSortByScore()
        {
            ListUser.Sort();
            return ListUser;
        }
        private async Task<StorageFile> CreateDefaultFile()
        {

            List<string> content = new List<string>();
            content.Add("<root>");
            content.Add("<ListUser>");

            content.Add("<User>");
            content.Add("<Name>User</Name>");
            content.Add("<LinkAvatar>ms-appx:///images/avatars/user3.png</LinkAvatar>");

            content.Add("<ListPoint>");
            //for (int i = 0; i < 6; i++)
            //    content.Add("<Point>0</Point>");

            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("<Point>0</Point>");
            content.Add("</ListPoint>");

            content.Add("</User>");
            content.Add("</ListUser>");
            content.Add("</root>");

            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync("users.xml", CreationCollisionOption.OpenIfExists);
            await FileIO.WriteLinesAsync(file, content.ToArray());
            return file;
        }
    }
}
