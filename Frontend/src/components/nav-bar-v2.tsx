import {
  Flex,
  Button,
  useColorMode,
  IconButton,
  useColorModeValue,
  Tooltip,
} from "@chakra-ui/react";
import { signOut } from "aws-amplify/auth";
import { useNavigate } from "react-router-dom";
import logo from "../assets/logo-dark.svg";
import { FaSun, FaMoon } from "react-icons/fa";
import { ArrowBackIcon } from "@chakra-ui/icons";

const NavBarV2 = () => {
  const { colorMode, toggleColorMode } = useColorMode();
  const navigate = useNavigate();
  const bgColor = useColorModeValue("primary.200", "secondary.200");
  const color = useColorModeValue("accent.100", "primary.200");
  const tooltipLabel =
    colorMode === "light"
      ? "Click to change to dark mode"
      : "Click to change to light mode";

  const handleLogout = async () => {
    await signOut();
  };

  const handleHomeClick = async () => {
    navigate(`/`);
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
        <Tooltip label="Home" fontSize="sm">
          <IconButton
            aria-label="Toggle color mode"
            icon={<ArrowBackIcon />}
            variant="solid"
            bg={colorMode === "light" ? "primary.100" : "primary.200"}
            color={colorMode === "light" ? "primary.200" : "accent.100"}
            onClick={handleHomeClick}
          />
        </Tooltip>

        <Tooltip label={tooltipLabel} fontSize="sm">
          <IconButton
            aria-label="Toggle color mode"
            icon={colorMode === "light" ? <FaMoon /> : <FaSun />}
            onClick={toggleColorMode}
            variant="solid"
            bg={colorMode === "light" ? "primary.100" : "primary.200"}
            color={colorMode === "light" ? "primary.200" : "accent.100"}
          />
        </Tooltip>
        <Button
          variant="solid"
          size="medium"
          onClick={handleLogout}
          color={colorMode === "light" ? "primary.200" : "accent.100"}
          bg={colorMode === "light" ? "primary.100" : "primary.200"}
        >
          Logout
        </Button>
      </Flex>
    </Flex>
  );
};

export default NavBarV2;
