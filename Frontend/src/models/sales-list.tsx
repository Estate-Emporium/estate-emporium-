export interface ListItems {
  id: number;
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

// export const mockData: ListItems[] = [
//   {
//     id: "1",
//     status: "Purchase Failed",
//     price: "1000000",
//     commission: "10000",
//   },
//   { id: "2", status: "Not started", price: "1500000", commission: "15000" },
//   {
//     id: "3",
//     status: "Awaiting home loan",
//     price: "2000000",
//     commission: "20000",
//   },
//   { id: "4", status: "Loan approved", price: "2500000", commission: "25" },
//   {
//     id: "5",
//     status: "Payment received",
//     price: "3000000",
//     commission: "30000",
//   },
//   {
//     id: "6",
//     status: "Ownership transfer complete",
//     price: "3500000",
//     commission: "35000",
//   },
//   {
//     id: "7",
//     status: "Persona Notified",
//     price: "4000000",
//     commission: "40000",
//   },
//   {
//     id: "8",
//     status: "Awaiting home loan",
//     price: "4500000",
//     commission: "45000",
//   },
//   { id: "9", status: "Loan approved", price: "5000000", commission: "50000" },
//   {
//     id: "10",
//     status: "Payment received",
//     price: "5500000",
//     commission: "55000",
//   },
// ];

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
