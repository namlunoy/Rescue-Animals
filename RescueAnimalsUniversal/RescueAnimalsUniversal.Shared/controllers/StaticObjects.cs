using System;
using System.Collections.Generic;
using System.Text;

namespace RescueAnimalsUniversal
{
    public static class StaticObjects
    {
        public static UserController userControll = new UserController();
        public static AnimalController animalControll = new AnimalController();
        public static AvatarController avatarControll = new AvatarController();
        public static StageController stageControll = new StageController();
        public static User currentUser = null;
    }
}
