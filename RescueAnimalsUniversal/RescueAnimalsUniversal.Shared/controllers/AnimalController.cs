using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RescueAnimalsUniversal
{
    [XmlRoot("root")]
    public class AnimalController
    {
        [XmlArray("ListAnimal")]
        [XmlArrayItem("Animal")]
        public List<Animal> ListAnimal = new List<Animal>();

        public async Task LoadDataAsync()
        {
            String content = await Helper.ReadFileAsync("data\\animals.xml");
            XDocument xDoc = XDocument.Parse(content);
            XmlSerializer se = new XmlSerializer(typeof(AnimalController));
            this.ListAnimal = (se.Deserialize(xDoc.CreateReader()) as AnimalController).ListAnimal;
        }

        //Lấy n con vật ra
        public List<Animal> GetAnimals(int n)
        {
            int i = 0;
            List<Animal> list = new List<Animal>();
            do
            {
                i = Helper.Rand.Next(ListAnimal.Count);
                if (!list.Contains(ListAnimal[i]))
                    list.Add(ListAnimal[i]);
            } while (list.Count < n);
            return list;
        }

        public Animal GetAnotherAnimal(Animal a)
        {
            while(true)
            {
                int k = Helper.Rand.Next(ListAnimal.Count);
                if (!a.Name.Equals(ListAnimal[k].Name))
                    return ListAnimal[k];
            }
        }


        public List<Animal> GetAnimalsWithSoundA(int n)
        {
            int i = 0;
            List<Animal> list = new List<Animal>();
            do
            {
                i = Helper.Rand.Next(ListAnimal.Count);
                if (!list.Contains(ListAnimal[i]) && ListAnimal[i].SoundAnimalUriString.Length > 10)
                    list.Add(ListAnimal[i]);
            } while (list.Count < n);
            return list;
        }

        public List<Animal> GetAnimalsWithSoundB(int n)
        {
            int i = 0;
            List<Animal> list = new List<Animal>();
            do
            {
                i = Helper.Rand.Next(ListAnimal.Count);
                if (!list.Contains(ListAnimal[i]) && ListAnimal[i].SoundHumanUriString.Length > 10)
                    list.Add(ListAnimal[i]);
            } while (list.Count < n);
            return list;
        }

        public List<Animal> GetAnimalsx2(int n)
        {
            n = n / 2;
            int i = 0;
            int count = 0;
            List<Animal> list = new List<Animal>();
            do
            {
                i = Helper.Rand.Next(ListAnimal.Count);
                if (!list.Contains(ListAnimal[i]))
                {
                    list.Add(ListAnimal[i]);
                    list.Add((ListAnimal[i].GetCopy()));
                    count++;
                }
            } while (count < n);
            List<Animal> list2 = TronAnimalsFromLists(list);
            return list2;
        }

        public List<Animal> TronAnimalsFromLists(List<Animal> l)
        {
            List<Animal> list = new List<Animal>(l);
            int n = l.Count, h, k;
            Animal t;
            for (int i = 0; i < n; i++)
            {
                k = Helper.Rand.Next(n);
                h = Helper.Rand.Next(n);
                while (h == k)
                    h = Helper.Rand.Next(n);

                t = list[h];
                list[h] = list[k];
                list[k] = t;

            }
            return list;
        }
    }
}
