export interface ListItems {
  saleId: number;
  price: number;
  commission: number;
  status: Step;
}

export type Step =
  | "Purchase Failed"
  | "Not started"
  | "Awaiting home loan"
  | "Loan approved"
  | "Payment received"
  | "Ownership transfer complete"
  | "Persona Notified";

export const statusDropDownData: Step[] = [
  "Purchase Failed",
  "Not started",
  "Awaiting home loan",
  "Loan approved",
  "Payment received",
  "Ownership transfer complete",
  "Persona Notified",
];

export const priceDropDownData: string[] = [
  "0 - 1,000,000",
  "1,000,000-2,000,000",
  "2,000,000-3,000,000",
  "4,000,000-5,000,000",
  "> 5,000,000",
];
