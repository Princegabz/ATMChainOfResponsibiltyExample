using System;

namespace ATMChainOfResponsibiltyExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create chain of handlers for the ATM
            ATMBase ATM = new HundredRandNote();
            ATMBase Fifty = new FiftyRandNote();
            ATMBase Twenty = new TwentyRandNote();
            ATMBase Ten = new TenRandNote();

            // Set successors in the chain
            ATM.SetSuccessor(Fifty);
            Fifty.SetSuccessor(Twenty);
            Twenty.SetSuccessor(Ten);

            // Output result of dispensing notes
            string? Result = null;

            // Example: Dispense 930 (should use Hundred, Fifty, Twenty, and Ten Rand notes)
            ATM.Dispense(940, ref Result);

            // Print the result
            Console.WriteLine(Result);
            Console.ReadLine();
        }
    }

    // Base class for ATM that defines the chain and dispense logic
    public abstract class ATMBase
    {
        protected ATMBase _successor;

        // Abstract method to be implemented by specific note handlers
        public abstract void Dispense(int Amount, ref string? Result);

        // Method to set the successor in the chain
        public void SetSuccessor(ATMBase successor)
        {
            _successor = successor;
        }
    }

    // Class to handle dispensing of 100 Rand notes
    public class HundredRandNote : ATMBase
    {
        public override void Dispense(int Amount, ref string? Result)
        {
            if (Amount >= 100) // If amount is 100 or more
            {
                int numNotes = Amount / 100; // Calculate how many 100 Rand notes
                Amount = Amount % 100; // Get the remainder
                Result += $"{numNotes} Hundred Rand Notes Dispensed\n";
            }

            // Pass remaining amount to the next handler if there is any remainder
            if (Amount > 0 && _successor != null)
            {
                _successor.Dispense(Amount, ref Result);
            }
        }
    }

    // Class to handle dispensing of 50 Rand notes
    public class FiftyRandNote : ATMBase
    {
        public override void Dispense(int Amount, ref string? Result)
        {
            if (Amount >= 50)
            {
                int numNotes = Amount / 50;
                Amount = Amount % 50;
                Result += $"{numNotes} Fifty Rand Notes Dispensed\n";
            }

            if (Amount > 0 && _successor != null)
            {
                _successor.Dispense(Amount, ref Result);
            }
        }
    }

    // Class to handle dispensing of 20 Rand notes
    public class TwentyRandNote : ATMBase
    {
        public override void Dispense(int Amount, ref string? Result)
        {
            if (Amount >= 20)
            {
                int numNotes = Amount / 20;
                Amount = Amount % 20;
                Result += $"{numNotes} Twenty Rand Notes Dispensed\n";
            }

            if (Amount > 0 && _successor != null)
            {
                _successor.Dispense(Amount, ref Result);
            }
        }
    }

    // Class to handle dispensing of 10 Rand notes
    public class TenRandNote : ATMBase
    {
        public override void Dispense(int Amount, ref string? Result)
        {
            if (Amount >= 10)
            {
                int numNotes = Amount / 10;
                Amount = Amount % 10;
                Result += $"{numNotes} Ten Rand Notes Dispensed\n";
            }

            // Since 10 is the smallest denomination, there's no need for a successor
        }
    }
}
