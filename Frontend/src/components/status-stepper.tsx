import React from "react";
import { Tooltip } from "@chakra-ui/react";
import "./stepper.css";

export type Step =
  | "Purchase Failed"
  | "Not started"
  | "Awaiting home loan"
  | "Loan approved"
  | "Payment received"
  | "Ownership transfer complete"
  | "Persona Notified";
interface StepperProps {
  currentStatus: Step;
}

function getCurrentState(index: number, currentStepIndex: number) {
  if (index < currentStepIndex) {
    return "completed";
  }
  if (index === currentStepIndex) {
    return "active";
  } else if (index > currentStepIndex) {
    return "incomplete";
  }
}

const StatusStepper: React.FC<StepperProps> = ({ currentStatus }) => {
  const steps: Step[] = [
    "Purchase Failed",
    "Not started",
    "Awaiting home loan",
    "Loan approved",
    "Payment received",
    "Ownership transfer complete",
    "Persona Notified",
  ];

  const currentStepIndex = steps.indexOf(currentStatus);

  return (
    <div className="stepper-container">
      <ol className="stepper">
        {steps.map((step, index) => (
          <Tooltip label={step} fontSize="sm">
            <li
              key={index}
              className={getCurrentState(index, currentStepIndex)}
            >
              {index + 1}
            </li>
          </Tooltip>
        ))}
      </ol>
    </div>
  );
};

export default StatusStepper;
