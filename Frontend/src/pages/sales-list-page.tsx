import { Card, CardBody, Flex, useColorMode, Heading } from "@chakra-ui/react";
import NavBarV2 from "../components/nav-bar-v2";
import ListCard from "../components/list-card";
import { Select } from "@chakra-ui/react";
import {
  ListItems,
  mockData,
  statusDropDownData,
  priceDropDownData,
} from "../models/sales-list";
import { useState } from "react";

const SalesPage = () => {
  const { colorMode } = useColorMode();
  const [selectedStatus, setSelectedStatus] = useState("");
  const [selectedPriceRange, setSelectedPriceRange] = useState("");

  const filteredData = mockData.filter((item: ListItems) => {
    // if a status filter is selected
    if (selectedStatus && selectedStatus !== "All") {
      if (item.status === selectedStatus) {
        // if a status filter is selected and a price filter is selected
        if (selectedPriceRange) {
          switch (selectedPriceRange) {
            case "0 - 1,000,000":
              return Number(item.price) <= 1000000;
            case "1,000,000-2,000,000":
              return (
                Number(item.price) > 1000000 && Number(item.price) <= 2000000
              );
            case "2,000,000-3,000,000":
              return (
                Number(item.price) > 2000000 && Number(item.price) <= 3000000
              );
            case "4,000,000-5,000,000":
              return (
                Number(item.price) > 4000000 && Number(item.price) <= 5000000
              );
            case "> 5,000,000":
              return Number(item.price) > 5000000;
            default:
              return item;
          }
        } else {
          return item;
        }
      }
    } else if (selectedPriceRange) {
      // if only a price filter is selected
      switch (selectedPriceRange) {
        case "0 - 1,000,000":
          return Number(item.price) <= 1000000;
        case "1,000,000-2,000,000":
          return Number(item.price) > 1000000 && Number(item.price) <= 2000000;
        case "2,000,000-3,000,000":
          return Number(item.price) > 2000000 && Number(item.price) <= 3000000;
        case "4,000,000-5,000,000":
          return Number(item.price) > 4000000 && Number(item.price) <= 5000000;
        case "> 5,000,000":
          return Number(item.price) > 5000000;
        default:
          return item;
      }
    } else {
      return item;
    }
  });

  return (
    <Flex
      width="100vw"
      height="100vh"
      maxWidth="100vw"
      maxHeight="100vh"
      flexDir="column"
      bg={colorMode === "light" ? "accent.100" : "primary.200"}
      color={colorMode === "light" ? "primary.200" : "accent.100"}
      overflow="hidden"
    >
      <NavBarV2 />
      <Flex height="90vh" width="100vw" align="center" flexDir="column">
        <Flex
          height="10vh"
          width="80vw"
          align="center"
          justify="flex-end"
          gap="2vw"
          p="2vh"
        >
          <Select
            placeholder="Select Status"
            width="15rem"
            value={selectedStatus}
            onChange={(e) => setSelectedStatus(e.target.value)}
          >
            {" "}
            {statusDropDownData.map((option, index) => (
              <option key={index} value={option}>
                {option}
              </option>
            ))}
          </Select>
          <Select
            placeholder="Select Price"
            width="15rem"
            value={selectedPriceRange}
            onChange={(e) => setSelectedPriceRange(e.target.value)}
          >
            {" "}
            {priceDropDownData.map((option, index) => (
              <option key={index} value={option}>
                {option}
              </option>
            ))}
          </Select>
        </Flex>
        <Flex flexDir="column" gap="3vh" overflow="auto" height="75vh" p="2vh">
          <Card
            width="80vw"
            justify="flex-start"
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
                paddingLeft="2vw"
                paddingRight="2vw"
              >
                <Heading textAlign="center" minWidth="8vw" fontSize="1.5rem">
                  ID
                </Heading>
                <Heading textAlign="center" minWidth="10vw" fontSize="1.5rem">
                  LIST PRICE
                </Heading>
                <Heading textAlign="center" minWidth="10vw" fontSize="1.5rem">
                  COMMISSION
                </Heading>
                <Heading textAlign="center" width="50%" fontSize="1.5rem">
                  STATUS
                </Heading>
              </Flex>
            </CardBody>
          </Card>
          {filteredData.map((item) => (
            <ListCard
              key={item.id}
              id={item.id}
              status={item.status}
              price={item.price}
              commission={item.commission}
            />
          ))}
        </Flex>
      </Flex>
    </Flex>
  );
};

export default SalesPage;
