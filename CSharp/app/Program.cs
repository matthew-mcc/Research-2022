using System;
using Phidget22;

namespace ConsoleApplication
{
	class Program
	{

		private static void VoltageInput0_VoltageChange(object sender, Phidget22.Events.VoltageInputVoltageChangeEventArgs e)
		{
			Console.WriteLine("Voltage: " + e.Voltage);
		}

		static void Main(string[] args)
		{
			VoltageInput voltageInput0 = new VoltageInput();

			voltageInput0.VoltageChange += VoltageInput0_VoltageChange;

			voltageInput0.Open(5000);

			//Wait until Enter has been pressed before exiting
			Console.ReadLine();

			voltageInput0.Close();
		}
	}
}