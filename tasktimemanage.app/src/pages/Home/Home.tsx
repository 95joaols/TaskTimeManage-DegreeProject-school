import { Flex, useDisclosure } from "@chakra-ui/react";
import CreateWorkItemModel from "../../components/Models/CreateWorkItemModel";
import UserInfo from "../../components/UserInfo";
import WorkItemMenu from "../../components/WorkItemMenu";




const Home = () => {

  const { isOpen, onOpen, onClose } = useDisclosure()

  return (
      <Flex>
        <Flex direction={"column"}>
          <UserInfo />
        <WorkItemMenu AddWorkItemPress={onOpen} />
        <CreateWorkItemModel isOpen={isOpen} onClose={ onClose}/>
        </Flex>
      </Flex>
  );
};

export default Home;
