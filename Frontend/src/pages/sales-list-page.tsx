import { Flex, useColorMode } from '@chakra-ui/react';
import NavBarV2 from '../components/nav-bar-v2';
import ListCard from '../components/list-card';
import { Select } from '@chakra-ui/react';

const SalesPage = () => {
  const { colorMode } = useColorMode();

  // const mockData = [
  //   { id: 1, status: "Purchase Failed", price: 100 },
  //   { id: 2, status: "Not started", price: 150 },
  //   { id: 3, status: "Awaiting home loan", price: 200 },
  //   { id: 4, status: "Loan approved", price: 250 },
  //   { id: 5, status: "Payment received", price: 300 },
  //   { id: 6, status: "Ownership transfer complete", price: 350 },
  //   { id: 7, status: "Persona Notified", price: 400 },
  //   { id: 8, status: "Awaiting home loan", price: 450 },
  //   { id: 9, status: "Loan approved", price: 500 },
  //   { id: 10, status: "Payment received", price: 550 },
  // ];

  return (
    <Flex
      width='100vw'
      height='100vh'
      maxWidth='100vw'
      maxHeight='100vh'
      flexDir='column'
      bg={colorMode === 'light' ? 'accent.100' : 'primary.200'}
      color={colorMode === 'light' ? 'primary.200' : 'accent.100'}
      overflow='hidden'
    >
      <NavBarV2 />
      <Flex height='90vh' width='100vw' align='center' flexDir='column'>
        <Flex
          height='10vh'
          width='100vw'
          align='center'
          justify='flex-end'
          gap='2vw'
          p='2vh'
        >
          <Select
            placeholder='Select Status'
            width='15vw'
            bg={colorMode === 'light' ? 'primary.200' : 'accent.100'}
            color={colorMode === 'light' ? 'accent.100' : 'primary.200'}
          >
            <option value='option1'>Purchase Failed</option>
            <option value='option2'>Not started</option>
            <option value='option3'>Awaiting home loan</option>
            <option value='option3'>Loan approved</option>
            <option value='option3'>Ownership transfer complete</option>
            <option value='option3'>Persona Notified</option>
          </Select>
          <Select
            placeholder='Select Price'
            width='15vw'
            bg={colorMode === 'light' ? 'primary.200' : 'accent.100'}
            color={colorMode === 'light' ? 'accent.100' : 'primary.200'}
          >
            <option value='option1'>Option 1</option>
            <option value='option2'>Option 2</option>
            <option value='option3'>Option 3</option>
          </Select>
        </Flex>
        <Flex flexDir='column' gap='3vh' overflow='auto' height='75vh' p='2vh'>
          <ListCard
            id='1'
            status='Purchase Failed'
            price='100'
            commission='10'
          />
        </Flex>
      </Flex>
    </Flex>
  );
};

export default SalesPage;
