import { Flex, useDisclosure } from "@chakra-ui/react";
import { useState } from "react";
import CreateWorkItemModel from "../../components/Models/CreateWorkItemModel";
import UserInfo from "../../components/UserInfo";
import WorkItemMenu from "../../components/WorkItemMenu";




const Home = () => {

  const { isOpen, onOpen, onClose } = useDisclosure()
  const [ActiveWorkItem, setActiveWorkItem] = useState<string>()

  return (
      <Flex>
        <Flex direction={"column"}>
          <UserInfo />
        <WorkItemMenu AddWorkItemPress={onOpen} onWorkItemPress={setActiveWorkItem} activeWorkItem={ ActiveWorkItem }/>
        <CreateWorkItemModel isOpen={isOpen} onClose={ onClose}/>
        </Flex>
      </Flex>
  );
};

export default Home;
