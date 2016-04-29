using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelConsumptionCalculator
{   /*
     * Calculates fuel consumption in l/100km and the equivalent mpg,
     * input units of measurement are litres (l) for the fuel used and
     * kilometres (km) for the distance travelled
     * 
     * Author: Michael Leontieff-Smith, n9455396
     * Date: 4 August 2015
     *  
     */
    class Program
    {
        const double MpGConvFact = 282.48;
        const int EXIT = 2;
        const int CALCULATE = 1;

        static void Main(string[] args)
        {
            int menuOption;
            do
            {
                Opener();
                menuOption = GrabMenuOption();
                PerformCalculation(menuOption);

            } while (menuOption != EXIT);

            ExitProgram();
        } // end Main

        static void Opener()
        {
            string message = "Welcome to the Fuel Consumption Calculator!\n\n"
                + "What would you like to do?\n\n"
                + "\t1) Calculate Fuel Consumption\n"
                + "\t2) Quit\n\n";
            Console.WriteLine(message);
        } // end Opener

        static int GrabMenuOption()
        {
            bool validOption;
            string stringInput;
            int optionOutput;
            do
            {
                Console.Write("Enter Menu Option: ");
                stringInput = Console.ReadLine();
                validOption = int.TryParse(stringInput, out optionOutput);
                if (!validOption)
                {
                    InputError(stringInput);
                }
                else if (validOption && (optionOutput <= 0 || optionOutput > 2))
                {
                    InputError(stringInput);
                    validOption = false;
                }

            } while (!validOption);

            return optionOutput;
        } // end GrabMenuOption

        static double GrabFuelValue()
        {
            bool validOption;
            string stringInput;
            double Output;
            do
            {
                Console.Write("Enter fuel consumed in litres: ");
                stringInput = Console.ReadLine();
                validOption = double.TryParse(stringInput, out Output);
                if (!validOption)
                {
                    InputError(stringInput);
                }
                else if (validOption && Output < 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! {0} is below the minimum of 20!", Output);
                    Console.ResetColor();
                    validOption = false;
                }

            } while (!validOption);

            return Output;
        } // end GrabFuelValue

        static double GrabDistanceValue(double fuelValue)
        {
            bool validOption;
            string stringInput;
            double Output, minDistance;
            minDistance = fuelValue * 8;
            do
            {
                Console.Write("Enter distance in kilometres: ");
                stringInput = Console.ReadLine();
                validOption = double.TryParse(stringInput, out Output);
                if (!validOption)
                {
                    InputError(stringInput);
                }
                else if (validOption && Output < (fuelValue * 8))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! {0} is below the minimum of {1}!", Output, minDistance);
                    Console.ResetColor();
                    validOption = false;
                }

            } while (!validOption);

            return Output;
        } // end GrabDistanceValue

        static void ExitProgram()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        } // end ExitProgram

        static void InputError(string stringInput)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error! '{0}' is not a valid input!", stringInput);
            Console.ResetColor();
        } // end InputError



        static void PerformCalculation(int menuOption)
        {
            if (menuOption == EXIT)
            {
                return;
            }
            double LitresMileage, GallonMileage;
            LitresMileage = LitresMileageConversion();
            GallonMileage = MPGConversion(LitresMileage);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nThat is {0:f4} litres per 100 kilometres", LitresMileage);
            Console.WriteLine("Or {0:f4} Miles per Galon (mpg)\n\n", GallonMileage);
            Console.ResetColor();
        } // end PerformCalculation

        static double MPGConversion(double LitresPer100k)
        {
            double milesPerGallon;
            milesPerGallon = MpGConvFact / LitresPer100k;
            return milesPerGallon;
        } // end MPGConversion

        static double LitresMileageConversion()
        {
            double LitresMileage;
            double fuelValue = GrabFuelValue();
            double distanceValue = GrabDistanceValue(fuelValue);
            LitresMileage = (fuelValue / distanceValue) * 100;
            return LitresMileage;
        } // end LitresMileageConversion
    }
}

