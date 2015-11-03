using System;

namespace UamTTA
{
    public class Transfer
    {
        public Transfer(decimal amount, Account sourceAccount, Account destinationAccount,
            Budget budget, DateTime? plannedDate, DateTime? transferDate,
            string reference = null)
        {
            Amount = amount;
            DestinationAccount = destinationAccount;
            SourceAccount = sourceAccount;
            Budget = budget;
            PlannedDate = plannedDate;
            TransferDate = transferDate;
            Reference = reference;
        }

        public Budget Budget { get; }

        public Account SourceAccount { get; }

        public Account DestinationAccount { get; }

        public decimal Amount { get; }

        public DateTime? PlannedDate { get; }

        public DateTime? TransferDate { get; }

        public string Reference { get; }

        public override string ToString()
        {
            return $"Budget: {Budget}, SourceAccount: {SourceAccount}, DestinationAccount: {DestinationAccount}, Amount: {Amount}, PlannedDate: {PlannedDate}, TransferDate: {TransferDate}, Reference: {Reference}";
        }
    }
}