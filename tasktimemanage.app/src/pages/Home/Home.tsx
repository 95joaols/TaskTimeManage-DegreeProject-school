import { Box, Button, Flex } from "@chakra-ui/react";
import UserInfo from "../../components/UserInfo";
import WorkItemMenu from "../../components/WorkItemMenu";


const OpenAddWorkItemModel = () => { };

const Home = () => {
  return (
      <Flex>
        <Flex direction={"column"}>
          <UserInfo />
          <WorkItemMenu AddWorkItemPress={OpenAddWorkItemModel}/>
        </Flex>
      </Flex>
  );
};

export default Home;
