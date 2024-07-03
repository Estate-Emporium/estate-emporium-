import React from 'react';
import { Tooltip } from '@chakra-ui/react';

type Step =
  | 'Purchase Failed'
  | 'Not started'
  | 'Awaiting home loan'
  | 'Loan approved'
  | 'Payment received'
  | 'Ownership transfer complete'
  | 'Persona Notified';
interface StepperProps {
  currentStatus: Step;
}

function getCurrentState(index: number, currentStepIndex: number) {
  if (index === currentStepIndex) {
    return 'active';
  }
  if (index < currentStepIndex) {
    return 'completed';
  }

  return '';
}

const StatusStepper: React.FC<StepperProps> = ({ currentStatus }) => {
  const steps: Step[] = [
    'Purchase Failed',
    'Not started',
    'Awaiting home loan',
    'Loan approved',
    'Payment received',
    'Ownership transfer complete',
    'Persona Notified',
  ];

  const currentStepIndex = steps.indexOf(currentStatus);

  console.log(currentStepIndex);

  return (
    <div className='stepper-container'>
      <ol className='stepper'>
        {steps.map((step, index) => (
          <Tooltip label={step} fontSize='sm'>
            <li key={step} className={getCurrentState(index, currentStepIndex)}>
              {index + 1}
            </li>
          </Tooltip>
        ))}
      </ol>
    </div>
  );
};

export default StatusStepper;
