<?php

require_once('global.php');
use wcf\data\user\User;
use wcf\data\user\group\UserGroup;

groupSync($_POST['FactionGroupList'], $_POST['GangGroupList'], $_POST['CompanyGroupList'], $_POST['Key']);

function groupSync($factionGroupList, $gangGroupList, $companyGroupList, $key) {
        $secretKey = ""; //128-Character Key

        if (strcmp($key, $secretKey) != 0)
        {
                $code = 1;
                $json = ["statusCode" => $code, "userData" => null];
                echo json_encode($json);
                return null;
        }

        foreach(json_decode($factionGroupList) as $userGroup)
        {
                $forumId = $userGroup->ForumId;
                $addList = $userGroup->AddGroupList;
                $removeList = $userGroup->RemoveGroupList;

                $users = User::getUsers([$forumId]);
                $user = array_pop($users);

                $action = new wcf\data\user\UserAction([$user], "addToGroups", [
                        'groups' => $addList,
                        'deleteOldGroups' => false,
                        'addDefaultGroups' => false]);
                $action->executeAction();

                $action = new wcf\data\user\UserAction([$user], "removeFromGroups", [
                        'groups' => $removeList]);
                $action->executeAction();
        }

        foreach(json_decode($gangGroupList) as $userGroup)
        {
                $forumId = $userGroup->ForumId;
                $addList = $userGroup->AddGroupList;
                $removeList = $userGroup->RemoveGroupList;

                $users = User::getUsers([$forumId]);
                $user = array_pop($users);
                $user->addToGroups($addList, false, false);
                $user->removeFromGroups($removeList);
        }

        foreach(json_decode($companyGroupList) as $userGroup)
        {
                $forumId = $userGroup->ForumId;
                $addList = $userGroup->AddGroupList;
                $removeList = $userGroup->RemoveGroupList;

                $users = User::getUsers([$forumId]);
                $user = array_pop($users);
                $user->addToGroups($addList, false, false);
                $user->removeFromGroups($removeList);
        }

        $code = 10;
        $json = ["statusCode" => $code, "userData" => null];
        echo json_encode($json);
        return null;
}

?>
