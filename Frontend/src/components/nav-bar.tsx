import {
  Flex,
  Button,
  useColorMode,
  IconButton,
  useColorModeValue,
  Tooltip,
} from "@chakra-ui/react";
import { signOut } from "aws-amplify/auth";
import logo from "../assets/text-logo.svg";
import { FaSun, FaMoon } from "react-icons/fa";

const NavBar = () => {
  const { colorMode, toggleColorMode } = useColorMode();
  const bgColor = useColorModeValue("secondary.200", "primary.200");
  const color = useColorModeValue("primary.200", "accent.100");
  const tooltipLabel =
    colorMode === "light"
      ? "Click to change to dark mode"
      : "Click to change to light mode";

  const handleLogout = async () => {
    await signOut();
  };

  return (
    <Flex
      justify="space-between"
      align="center"
      bg={bgColor}
      color={color}
      p="2vh"
      height="10vh"
      width="100vw"
    >
      <img src={logo} alt="logo" />
      <Flex align="center" gap="1vw">
        <Tooltip label={tooltipLabel} fontSize="sm">
          <IconButton
            aria-label="Toggle color mode"
            icon={colorMode === "light" ? <FaMoon /> : <FaSun />}
            onClick={toggleColorMode}
            variant="solid"
            bg={color}
            color={bgColor}
          />
        </Tooltip>
        <Button variant="solid" size="medium" onClick={handleLogout}>
          Logout
        </Button>
      </Flex>
    </Flex>
  );
};

export default NavBar;
