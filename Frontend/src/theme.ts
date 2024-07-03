import { ThemeConfig, extendTheme } from "@chakra-ui/react";

const config: ThemeConfig = {
  initialColorMode: "light",
  useSystemColorMode: false,
};

const customTheme = extendTheme({
  config,
  fonts: {
    heading: "Montserrat, sans-serif",
    body: "Varela, serif",
  },
  colors: {
    primary: {
      100: "#CAD5D9",
      200: "#12222B",
    },
    secondary: {
      100: "#44515C",
      200: "#6C869E",
    },
    accent: {
      100: "#F5FFFF",
    },
  },
  styles: {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    global: (props: any) => ({
      "html, body": {
        color: props.colorMode === "dark" ? "accent.100" : "primary.200",
        bg: props.colorMode === "dark" ? "primary.200" : "accent.100",
      },
      a: {
        color: props.colorMode === "dark" ? "accent.100" : "primary.200",
      },
    }),
  },
  textStyles: {
    h1: {
      fontSize: "6rem",
      fontWeight: "700",
    },
    h2: {
      fontSize: "4rem",
      fontWeight: "600",
    },
    h3: {
      fontSize: "3rem",
      fontWeight: "500",
    },
    h4: {
      fontSize: "1rem",
      fontWeight: "400",
    },
    h6: {
      fontSize: "0.5rem",
      fontWeight: "500",
    },
    body: {
      fontSize: "1rem",
      fontWeight: "400",
    },
  },
  components: {
    Button: {
      baseStyle: {
        fontWeight: "700",
        fontSize: "0.75rem",
        width: "fit-content",
        p: "2vh",
      },
      sizes: {
        large: {
          borderRadius: "10px",
          height: "5.75rem",
          fontSize: "1.75rem",
          width: "18.5rem",
        },
        medium: {
          borderRadius: "4px",
          width: "11rem",
          fontSize: "1rem",
          height: "2.5rem",
        },
        small: {
          borderRadius: "4px",
          height: "2.5rem",
          fontSize: "1rem",
        },
      },
      variants: {
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        solid: (props: any) => ({
          bg: props.colorMode === "dark" ? "accent.100" : "primary.200",
          color: props.colorMode === "dark" ? "primary.200" : "accent.100",
          _hover: {
            opacity: 0.95,
          },
        }),
      },
    },
  },
});

export default customTheme;
