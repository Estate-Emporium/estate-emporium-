import { Button, Flex, Heading, useColorMode } from "@chakra-ui/react";
import { useNavigate } from "react-router-dom";
import NavBar from "../components/nav-bar";
import logo from "../assets/logo.svg";

const LandingPage = () => {
  const { colorMode } = useColorMode();
  const navigate = useNavigate();

  const handleViewListingClick = () => {
    navigate(`/sales-list`);
  };

  return (
    <Flex
      width="100vw"
      height="100vh"
      flexDir="column"
      bg={colorMode === "light" ? "accent.100" : "primary.100"}
      color={colorMode === "light" ? "primary.200" : "accent.100"}
    >
      <NavBar />
      <Flex
        height="90vh"
        width="100vw"
        align="center"
        justify="center"
        backgroundImage={
          colorMode === "light"
            ? "url('/src/assets/background.png')"
            : "url('/src/assets/background-dark.png')"
        }
        backgroundSize="cover"
        backgroundPosition="center"
      >
        <Flex
          height="70vh"
          width="65vw"
          gap="5vh"
          justify="center"
          align="center"
          flexDir="column"
          className="gradient-container"
          borderRadius="4px"
          border="0.1rem solid"
          borderColor="#44515C33"
          style={{
            background: `linear-gradient(to bottom left, ${
              colorMode === "light" ? "#12222BFF" : "#44515CFF"
            } 0%, ${colorMode === "light" ? "#44515C66" : "#12222B66"} 100%)`,
          }}
        >
          <img src={logo} alt="logo" />
          <Heading
            textStyle="h1"
            color="accent.100"
            textAlign="center"
            width="50%"
            fontWeight="600"
          >
            Unlock the door to your future
          </Heading>
          <Button
            size="large"
            variant="solid"
            fontWeight="700"
            bg={colorMode === "light" ? "primary.200" : "accent.100"}
            color={colorMode === "light" ? "accent.100" : "primary.200"}
            onClick={handleViewListingClick}
          >
            View Listings
          </Button>
        </Flex>
      </Flex>
    </Flex>
  );
};

export default LandingPage;
