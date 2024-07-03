import { Flex, useColorMode } from '@chakra-ui/react';
import NavBar from '../components/nav-bar';
import StatusStepper from '../components/status-stepper';
import './stepper.css';

const SalesPage = () => {
  const { colorMode } = useColorMode();

  return (
    <Flex
      width='100vw'
      height='100vh'
      flexDir='column'
      bg={colorMode === 'light' ? 'accent.100' : 'primary.100'}
      color={colorMode === 'light' ? 'primary.200' : 'accent.100'}
    >
      <NavBar />
      <Flex height='90vh' width='100vw' align='center' justify='center'>
        <StatusStepper currentStatus='In Progress' />
      </Flex>
    </Flex>
  );
};

export default SalesPage;
