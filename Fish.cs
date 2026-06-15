using Barbie_on_the_Kama.Properties;
using System;
using System.Collections.Generic;

namespace Barbie_on_the_Kama
{
    public class Fish
    {
        public string Name;
        public int MinWeight;
        public int MaxWeight;
        public double Points;
        public double Money;
        public int ZachetWeight; 
        public int TrophyWeight;
        public System.Drawing.Image Image;

        public Fish(string name, int minweight, int maxweight, double points, double money, int zachetWeight, int trophyWeight, System.Drawing.Image image = null)
        {
            Name = name;
            MinWeight = minweight;
            MaxWeight = maxweight;
            Points = points;
            Money = money;
            ZachetWeight = zachetWeight;
            TrophyWeight = trophyWeight;
            Image = image;
        }
    }

    public static class FishData
    {
        public static Dictionary<string, Dictionary<Fish, int>> LocationProbabilities = new Dictionary<string, Dictionary<Fish, int>>();
        public static Dictionary<string, Dictionary<Fish, int>> BaitPreferences = new Dictionary<string, Dictionary<Fish, int>>();

        public static Fish Плотва = new Fish("Плотва", 60, 813, 0.013, 0.0048, 300, 650, Resources.plotva);
        public static Fish Окунь = new Fish("Окунь", 40, 1125, 0.013, 0.00415, 400, 850 , Resources.okun);
        public static Fish Елец = new Fish("Елец", 20, 312, 0.03, 0.0108, 125, 250, Resources.elec);
        public static Fish Подкаменщик = new Fish("Подкаменщик", 8, 45, 1.06, 0.021, 20, 40, Resources.pdkm);
        public static Fish Голавль = new Fish("Голавль", 100, 3222, 0.0095, 0.002, 1000, 2500, Resources.golavl);
        public static Fish Язь = new Fish("Язь", 100, 2531, 0.0075, 0.0019, 1000, 2150, Resources.yaz);
        public static Fish Подуст = new Fish("Подуст", 50, 1323, 0.085, 0.0086, 300, 1000, Resources.podust);
        public static Fish Хариус = new Fish("Хариус", 50, 1250, 0.025, 0.00968, 300, 800, Resources.khar);
        public static Fish Пескарь = new Fish("Пескарь", 8, 79, 0.085, 0.025, 30, 70, Resources.peskar);
        public static Fish Гольян = new Fish("Гольян", 5, 52, 0.085, 0.02, 20, 45, Resources.golyan);
        public static Fish Таймень = new Fish("Таймень", 2000, 26000, 0.0115, 0.00312, 7000, 20000, Resources.tai);
        public static Fish Жерех = new Fish("Жерех", 400, 6100, 0.011, 0.0067, 2000, 5000, Resources.zherix);
        public static Fish Щука = new Fish("Щука", 200, 8888, 0.0075, 0.002, 2000, 6000, Resources.schuka);
        public static Fish Ручьевая_форель = new Fish("Ручьевая форель", 50, 700, 0.625, 0.0555, 250, 600, Resources.trout);


        public static Dictionary<string, int> LocationDepth = new Dictionary<string, int>()
        {
            {"Пая", 320},
            {"Косьва", 370},
            {"Сюзьва", 420},
            {"Сылва", 390},
            {"Обва", 350},
            {"Ирень", 373},
            {"Чусовая", 384},
            {"Вишера", 354}
        };


        static FishData()
        {
            LocationProbabilities["Пая"] = new Dictionary<Fish, int>
            {
                [Гольян] = 700,
                [Хариус] = 200,
                [Елец] = 97,
                [Подкаменщик] = 3
            };

            LocationProbabilities["Косьва"] = new Dictionary<Fish, int>
            {
                [Окунь] = 300,
                [Плотва] = 540,
                [Язь] = 100,
                [Жерех] = 50,
                [Щука] = 10
            };

            LocationProbabilities["Сюзьва"] = new Dictionary<Fish, int>
            {
                [Окунь] = 250,
                [Плотва] = 250,
                [Елец] = 50,
                [Голавль] = 100,
                [Щука] = 20,
                [Пескарь] = 100,
                [Язь] = 230
            };

            LocationProbabilities["Сылва"] = new Dictionary<Fish, int>
            {
                [Пескарь] = 180,
                [Подуст] = 20,
                [Плотва] = 300,
                [Щука] = 100,
                [Елец] = 50,
                [Окунь] = 300,
                [Хариус] = 50
            };

            LocationProbabilities["Обва"] = new Dictionary<Fish, int>
            {
                [Гольян] = 250,
                [Пескарь] = 250,
                [Хариус] = 50,
                [Голавль] = 150,
                [Елец] = 50,
                [Подуст] = 15,
                [Окунь] = 185,
                [Щука] = 50
            };

            LocationProbabilities["Ирень"] = new Dictionary<Fish, int>
            {
                [Голавль] = 340,
                [Гольян] = 350,
                [Плотва] = 100,
                [Язь] = 190,
                [Ручьевая_форель] = 20
            };

            LocationProbabilities["Чусовая"] = new Dictionary<Fish, int>
            {
                [Голавль] = 150,
                [Плотва] = 100,
                [Окунь] = 100,
                [Щука] = 50,
                [Подуст] = 150,
                [Хариус] = 200,
                [Елец] = 248,
                [Таймень] = 2
            };

            LocationProbabilities["Вишера"] = new Dictionary<Fish, int>
            {
                [Хариус] = 400,
                [Щука] = 240,
                [Жерех] = 100,
                [Пескарь] = 95,
                [Гольян] = 100,
                [Окунь] = 40,
                [Подкаменщик] = 10,
                [Таймень] = 15
            };

            BaitPreferences["Червь"] = new Dictionary<Fish, int>
            {
                [Плотва] = 50,
                [Окунь] = 90,
                [Елец] = 50,
                [Подкаменщик] = 100,
                [Голавль] = 35,
                [Язь] = 50,
                [Подуст] = 10,
                [Хариус] = 20,
                [Пескарь] = 75,
                [Гольян] = 5,
                [Таймень] = 1,
                [Жерех] = 0,
                [Щука] = 1,
                [Ручьевая_форель] = 5
            };

            BaitPreferences["Опарыш"] = new Dictionary<Fish, int>
            {
                [Плотва] = 50,
                [Окунь] = 5,
                [Елец] = 60,
                [Подкаменщик] = 40,
                [Голавль] = 10,
                [Язь] = 10,
                [Подуст] = 15,
                [Хариус] = 60,
                [Пескарь] = 70,
                [Гольян] = 90,
                [Таймень] = 0,
                [Жерех] = 1,
                [Щука] = 0,
                [Ручьевая_форель] = 10
            };

            BaitPreferences["Кузнечик"] = new Dictionary<Fish, int>
            {
                [Плотва] = 3,
                [Окунь] = 1,
                [Елец] = 20,
                [Подкаменщик] = 0,
                [Голавль] = 80,
                [Язь] = 65,
                [Подуст] = 30,
                [Хариус] = 60,
                [Пескарь] = 0,
                [Гольян] = 0,
                [Таймень] = 3,
                [Жерех] = 3,
                [Щука] = 0,
                [Ручьевая_форель] = 55
            };

            BaitPreferences["Ручейник"] = new Dictionary<Fish, int>
            {
                [Плотва] = 20,
                [Окунь] = 40,
                [Елец] = 80,
                [Подкаменщик] = 25,
                [Голавль] = 45,
                [Язь] = 45,
                [Подуст] = 90,
                [Хариус] = 70,
                [Пескарь] = 10,
                [Гольян] = 1,
                [Таймень] = 5,
                [Жерех] = 0,
                [Щука] = 0,
                [Ручьевая_форель] = 45
            };

            BaitPreferences["Живец"] = new Dictionary<Fish, int>
            {
                [Плотва] = 0,
                [Окунь] = 5,
                [Елец] = 0,
                [Подкаменщик] = 0,
                [Голавль] = 0,
                [Язь] = 5,
                [Подуст] = 0,
                [Хариус] = 1,
                [Пескарь] = 0,
                [Гольян] = 0,
                [Таймень] = 80,
                [Жерех] = 70,
                [Щука] = 70,
                [Ручьевая_форель] = 2
            };
        }
    }
}