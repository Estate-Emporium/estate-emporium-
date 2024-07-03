import { Flex } from '@chakra-ui/react';
import StatusStepper from './status-stepper';

const ListCard = () => {
  // const { colorMode } = useColorMode();
  return (
    <Flex width={'100%'}>
      <StatusStepper currentStatus='Awaiting home loan' />
    </Flex>
  );
};

export default ListCard;
