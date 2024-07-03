import React from 'react';

type Step = 'Pending' | 'In Progress' | 'Sold';
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
  const steps: Step[] = ['Pending', 'In Progress', 'Sold'];

  const currentStepIndex = steps.indexOf(currentStatus);

  console.log(currentStepIndex);

  return (
    <div className='stepper-container'>
      <ol className='stepper'>
        {steps.map((step, index) => (
          <li key={step} className={getCurrentState(index, currentStepIndex)}>
            {step}
          </li>
        ))}
      </ol>
    </div>
  );
};

export default StatusStepper;
