import { Card, CardBody, Flex, Text, useColorMode } from '@chakra-ui/react';
import StatusStepper, { Step } from './status-stepper';

type ListItems = {
  id: string;
  price: string;
  commission: string;
  status: Step;
};

const ListCard = (listItems: ListItems) => {
  const { colorMode } = useColorMode();
  return (
    <Card
      width='80vw'
      style={{
        background: `linear-gradient(to top right, ${
          colorMode === 'light' ? '#6C869E42' : '#CAD5D942'
        } 0%, ${colorMode === 'light' ? '#12222B42' : '#44515C42'} 100%)`,
      }}
    >
      <CardBody>
        <Flex flexDir='row' justify='space-between' align='center'>
          <Text>{listItems.id}</Text>
          <Text>{listItems.price}</Text>
          <Text>{listItems.commission}</Text>
          <StatusStepper currentStatus={listItems.status} />
        </Flex>
      </CardBody>
    </Card>
  );
};

export default ListCard;
