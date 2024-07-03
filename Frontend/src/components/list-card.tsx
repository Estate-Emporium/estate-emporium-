import { Card, CardBody, Flex, Text, useColorMode } from "@chakra-ui/react";
import StatusStepper from "./status-stepper";
import { ListItems } from "../models/sales-list";

const ListCard = (listItems: ListItems) => {
  const { colorMode } = useColorMode();
  return (
    <Card
      width="80vw"
      style={{
        background: `linear-gradient(to right, ${
          colorMode === "light" ? "#6C869EFF" : "#CAD5D966"
        } 0%, ${colorMode === "light" ? "#44515C66" : "#44515CFF"} 100%)`,
      }}
    >
      <CardBody>
        <Flex
          flexDir="row"
          justify="space-between"
          align="center"
          flexWrap="wrap"
        >
          <Flex
            flexDir="row"
            justify="space-between"
            paddingLeft="2vw"
            paddingRight="2vw"
          >
            <Text textAlign="center" minWidth="8vw">
              {listItems.id}
            </Text>
            <Text textAlign="center" minWidth="10vw">
              {listItems.price}
            </Text>
            <Text textAlign="center" minWidth="10vw">
              {listItems.commission}
            </Text>
          </Flex>
          <StatusStepper currentStatus={listItems.status} />
        </Flex>
      </CardBody>
    </Card>
  );
};

export default ListCard;
