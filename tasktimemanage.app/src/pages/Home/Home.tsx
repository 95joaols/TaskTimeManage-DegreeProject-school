import { Flex } from "@chakra-ui/react";
import { Box, Center } from "@chakra-ui/layout";

import UserInfo from "../../components/UserInfo";

const Home = () => {
  return (
    <Flex>
      <UserInfo />
    </Flex>
  );
};

export default Home;
