using RGBSzinek.Models;
using System;

namespace RGBSzinek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Olvassa be a kep.txt állomány tartalmát, és tárolja el a 640×360 képpont színét!
            // Ezt a Manager osztály LoadFromTXT metódusa oldja meg!
            RGBpicManager manager = new RGBpicManager();
            manager.LoadFromTXT("Datas\\kep.txt");

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            // 2. Kérje be a felhasználótól a kép egy pontjának sor- és oszlopszámát (a számozás mindkét
            // esetben 1 - től indul), és írja a képernyőre az adott képpont RGB színösszetevőit a minta
            // szerint!

            Console.WriteLine("2. feladat");
            Console.WriteLine("Kérem egy képpont adatait!");
            Console.Write("Sor [1..360] :");
            int sorIndex = int.Parse(Console.ReadLine());  //Nem szép, mivel a null eset nincs kezelve!

            //Szép megoldás:
            //int sorIndex;
            //int.TryParse(Console.ReadLine(), out sorIndex);

            Console.Write("Oszlop [1..640] :");
            int oszlopIndex = int.Parse(Console.ReadLine());




            RGBpixel melyik = manager.Pixelek[oszlopIndex, sorIndex];
            Console.WriteLine(melyik.ToString()); //Fontos, hogy az RGBpixel osztályban megtörténjen előtte az override !

            //Console.WriteLine(melyik); // Így is jó, mivel a ToString() implicit hívásra kerül.
            //Console.WriteLine(manager.Pixelek[oszlopIndex, sorIndex]); // Röviden


            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            // 3. Világosnak tekintjük az olyan képpontot, amely RGB-értékeinek összege 600-nál nagyobb.
            // Számolja meg és írja ki, hogy a teljes képen hány világos képpont van!

            // Előbb bővítem az RGBpixel osztály tudását egy IsLight() metódussal

            // Ha nem akarok állandóan két egymásba ágyazott ciklussal 2 dimenziós tömböt bejárni, akkor okosíthatom
            // a Manager osztályt egy olyannal, ami listába szervezi a pixeleket. pld. PixelList néven.
            // Ennek az lesz az előnye, hogy LINQ eszközöket tudok használni.

            Console.WriteLine("3. feladat");
            var pixelCount = manager.PixelList.Count(p => p.IsLight() == true); // LINQ eszközökkel

            Console.WriteLine($"A világos képpontok száma: {pixelCount}");

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            // 4.A kép legsötétebb pontjainak azokat a pontokat tekintjük, amelyek RGB-értékeinek összege
            // a legkisebb.Adja meg, hogy mennyi a legkisebb összeg, illetve keresse meg az ilyen RGB
            // összegű pixeleket, és írja ki mindegyik színét RGB(r, g, b) formában a mintának
            // megfelelően!
            Console.WriteLine("4. feladat");

            // Az RGBpixel osztályt kiegészítjük egy ColorSum property-vel, ami visszaadja a pixel RGB összegét.
            int minColor = manager.PixelList.Min(p => p.ColorSum);

            Console.WriteLine($"A legsötétebb pont RGB összege: {minColor}");

            // Ha megvan a legsötétebb pixel RGB összege, akkor keressük meg azokat a pixeleket, amiknek ez az RGB összege.
            Console.WriteLine("A legsötétebb pixelek színe:");
            foreach (var pixel in manager.PixelList)
            {
                if (pixel.ColorSum == minColor)
                {
                    Console.WriteLine(pixel.ToString());
                }
            }

            //Egyetlen LINQ utasítással is megoldható:
            //manager.PixelList
            //    .Where(p => p.ColorSum == minColor)
            //    .ToList()
            //    .ForEach(p => Console.WriteLine(p));

            // Az eredeti feladatban nem kellett a pozíciót feltüntetni, nehezítésként most ezt is megcsinálom.
            // Most nem a PixelList-tel dolgozom, hanem a Pixelek 2 dimenziós tömbjével.
            Console.WriteLine("\nKIEGÉSZÍTŐ MEEGOLDÁS!\nA legsötétebb pixelek színe és pozíciója:");
            for (int i = 1; i < manager.Pixelek.GetLength(0); i++)
            {
                for (int j = 1; j < manager.Pixelek.GetLength(1); j++)
                {
                    if (manager.Pixelek[i, j].ColorSum == minColor)
                    {
                        Console.WriteLine($"Sor: {i}, Oszlop: {j}, Szín: {manager.Pixelek[i, j]}");
                    }
                }
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            // 5.feladat
            //A képen a kék ég látható közepén egy felhővel.Az ég és a felhő színe között jelentős
            //különbség van, így az ég-felhő határvonal programmal is felismerhető.Ennek
            //megtalálásához készítsen függvényt hatar néven, amely megadja, hogy egy adott sorban
            //van - e olyan hely a képen, ahol az egymás melletti képpontok kék színösszetevőinek eltérése
            //meghalad egy adott értéket! A függvény kapja meg paraméterként a sor számát, illetve
            //az eltérés értékét, melyek egészek!A függvény visszatérési értéke egy logikai érték legyen,
            //amely megadja, hogy az adott sorban volt-e az eltérést meghaladó különbség az egymás
            //melletti képpontok kék színében!

            // A Manager osztályt kiegészítjük egy VanHatarAzEgben() metódussal! Ez megadja, hogy egy adott sorban
            // van-e olyan hely, ahol az egymás melletti képpontok KÉK színösszetevőinek eltérése meghaladja-e
            // a megadott értéket!

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


            // 6.feladat

            //Keresse meg a képen a felhő első és utolsó sorát az előzőleg elkészített függvény
            //segítségével úgy, hogy eltérésként 10 - et ad meg a függvénynek bemenetként! Adja meg
            //az első és az utolsó olyan sor sorszámát, ahol az eltérés a soron belül valahol 10 - nél
            //nagyobb!

            // Fontos megjegyzések:
            // Feltételezzük, hogy a felhő egybefüggő, és a képen csak egy felhő van!
            // Ha több felhő lenne egymás alatt, akkor csak az első felhőt tudnánk megtalálni.

            bool felhobenVan = false;  //Azt jelzi, hogy a felhőben vagyunk-e már?

            int kepMagassaga = manager.Pixelek.GetLength(1) - 1; //A 0-ás index nem számít!
            // Szebb lenne egy külön property létrehozása ilyenkor, ami visszaadja a képméretet.
            // int kepMagassaga = manager.KepMagassaga; // Ha lenne ilyen property
            // A Manager osztályban ezt a következőképpen tudnánk megoldani:
            // public int KepMagassaga=> this.Pixelek.GetLength(1) - 1;

            int elsoFelhocsucs = 0; // A felhő legfelső sora
            int utolsoFelhocsucs = 0; // A felhő legalsó sora
            int elteres = 10; // A megadott eltérés felett keressük a felhő határvonalát

            for (int i = 1; i <= kepMagassaga; i++)
            {
                if (manager.VanHatarAzEgben(i, elteres) && !felhobenVan)
                {
                    elsoFelhocsucs = i;
                    felhobenVan = true;
                }
                else if (manager.VanHatarAzEgben(i, elteres) && felhobenVan)
                {
                    utolsoFelhocsucs = i;
                }
            }

            Console.WriteLine("6. feladat");
            Console.WriteLine($"A felhő legfelső sora: {elsoFelhocsucs}");
            Console.WriteLine($"A felhő legalsó sora: {utolsoFelhocsucs}");

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Console.WriteLine("\n\n EXTRA");
            manager.SaveToTXT("Datas\\kimenet.txt");

        }
    }
}
