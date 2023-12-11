// See https://aka.ms/new-console-template for more information

using System.Threading.Tasks;
using System;

Console.WriteLine("+--------------+");
Console.WriteLine("|    WELCOME   |");
Console.WriteLine("+--------------+");
Console.WriteLine("");


// This section prompts the user for their income, tax, expenses, rent, or buy.
Console.WriteLine("");
Console.WriteLine("=======================================================================================================");
// GetValidInput() re-prompts for input in case of an error.
int monthIncome = GetValidInput("Please enter your Gross Monthly Income (Before deductions): "); 


Console.WriteLine("");
Console.WriteLine("=======================================================================================================");
int monthTax = GetValidInput("Please enter your Estimated Monthly Tax (if it's 1%, enter 1: ");

// To calculate the tax, we divide the tax percentage by 100 and multiply it by the monthly income. Then we minus the tax amount from the income.
double taxDecimal = monthTax / 100.0;  // Because this is a double, we add .0 after 100, although it functions the same as 100.
int taxAmountIncome = (int) (monthIncome * taxDecimal); // The (int) allows calculations involving integers and doubles.
int monthIncomeAfterTax = (monthIncome - taxAmountIncome);


Console.WriteLine("");
Console.WriteLine("=======================================================================================================");
int monthExpenses = GetValidInput("Please enter your Estimated Monthly Expenditures (Monthly expenses): ");

int afterDeductions = monthIncomeAfterTax - monthExpenses;

 GetRentOrBuyOption(monthIncome, afterDeductions);

// This method is designed to re-prompt the user if an error occurs.
static int GetValidInput(string prompt)
{
    while (true)
    {
        try
        {
            Console.WriteLine(prompt);
            return Convert.ToInt32(Console.ReadLine()); //By default, input is considered a string; we need to convert it to a number.
        }
        catch (FormatException)
        {
            // This error will display if a decimal or string is entered.
            Console.WriteLine("ERROR: Invalid input. Please enter a whole number (Not Decimal numbers).");
        }
    }
}

// This method handles the options for renting or buying.
static void GetRentOrBuyOption(int afterDeductions, int monthIncome)
{
    while (true)
    {
        Console.WriteLine("*******************************************************************************************************");
        Console.WriteLine("Do you want to Rent or Buy a property?");
        Console.WriteLine();

        int option = GetValidInput("Enter 1 to Rent or 2 to Buy: ");

        if (option == 1)
        {
            CalcRent(monthIncome, afterDeductions);
            break; // Exit the loop after CalcRent is executed
        }

        else if (option == 2)
        {
            CalcBuy(monthIncome, afterDeductions);
            break; // Exit the loop after CalcBuy is executed
        }
        else {
            Console.WriteLine("*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*!*");
            Console.WriteLine("ERROR: Invalid number.");
        }

    }
}

// Option 1 - Rent calculations
static void CalcRent(int afterDeductions, int monthIncome) // monthIncome is the second paramater becuase this methods does not use the variable
{
    Console.WriteLine();
    int monthRentAmount = GetValidInput("Enter monthly rental amount");

    int totAfterRent = afterDeductions - monthRentAmount;

    if (totAfterRent <= 0)
    {
        Console.WriteLine();
        Console.WriteLine("*******************************************************************************************************");
        Console.WriteLine($"You do not have sufficient funds. You will need an additional R{totAfterRent} to pay the rent.");
    }


    if (totAfterRent > 0)
    {
        Console.WriteLine();
        Console.WriteLine("*******************************************************************************************************");
        Console.WriteLine($"After paying the rent, your remaining balance will be R{totAfterRent}.");
    }
}

// Option 2 - Buy calculation
static void CalcBuy(int afterDeductions, int monthIncome)
{
    Console.WriteLine("=======================================================================================================");
    int purchaseAmount = GetValidInput("Enter Purchase price (Total cost of the property): ");

    Console.WriteLine("=======================================================================================================");
    int depositAmount = GetValidInput("Enter Deposit amount, if not applicable enter 0: ");

    Console.WriteLine("=======================================================================================================");
    int interestAmount = GetValidInput("Enter Interest rate (if it's 1%, enter 1: ");

    Console.WriteLine("=======================================================================================================");
    int monthsAmount = GetValidInput("Enter the home loan duration in months : ");

    
    int loanAmount= purchaseAmount - depositAmount; // This will determine the loan amount.

    // This will calculate the interest.
    double taxDecimal = interestAmount / 100.0;  //Because this is a double, we add .0 after 100, although it functions the same as 100. 
    int interestAmountOnLoan = (int)(loanAmount * taxDecimal); //This will calculate the property's interest.

    // This will calculate the interest accumulated over the loan duration.
    int yearsPayBack = monthsAmount / 12; // This will calculate the loan duration in years.
    int totalInterstOverYears = interestAmountOnLoan * yearsPayBack;  // This will calculate the total interest accrued over the years.
    int totalAmountPayBack = totalInterstOverYears + loanAmount;  // This will combine the interest with the purchase price.
    int monthlyPayment = (totalAmountPayBack / monthsAmount); // This will help us determine the monthly payment installment.


    // This will check if the repayment amount exceeds one-third of the monthly income.
    int oneThirdMonthIncome = monthIncome / 3; // Calculates one-third of the monthly income 


    if (oneThirdMonthIncome <= monthlyPayment)
    {
        Console.WriteLine("");
        Console.WriteLine("******************************************************************************************************************");
        Console.WriteLine($"The monthly repayment of R{monthlyPayment} exceeds one-third of your monthly income. A home loan is not feasible.");
    }
    if (oneThirdMonthIncome > monthlyPayment)
    {
        int incomeRemaining = afterDeductions - monthlyPayment; // this will calculate how much money is left after paying the monthly installment.

        Console.WriteLine("");
        Console.WriteLine("**********************************************************************************************************************");
        Console.WriteLine($"The monthly repayment of R{monthlyPayment} does not exceed one-third of your monthly income. A home loan is possible.");
        Console.WriteLine($"After paying the monthly installment, your remaining balance will be R{incomeRemaining}.");
    }
}


/*
 static void formulaCalc()
{
    Console.Write("Enter Purchase price (Total cost of the property "); 
    double purchaseAmount = Convert.ToDouble(Console.ReadLine());

    Console.Write("Enter annual interest rate (as a decimal): ");
    double annualInterestRate = Convert.ToDouble(Console.ReadLine());

    Console.Write("Enter loan duration in months: ");
    int months = Convert.ToInt32(Console.ReadLine());

     double monthlyInterestRate = annualInterestRate / 12; // This will calculate the montly interest amount 
     double numerator = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, months);
     double denominator = Math.Pow(1 + monthlyInterestRate, months) - 1;

     double monthlyRepayment = purchaseAmount * (numerator / denominator);

     Console.WriteLine($"Your monthly repayment is: {monthlyRepayment:C}");
}
formulaCalc();
*/

