# Install/uninstall Docker on Ubuntu on Hyper-V:

1: Navigate to start menu and open Hyper-v Manager.

![](/source/docs/images/install-uninstall-docker/01-Hyper-v.png)

2.1 Select your Linux virtual machine. </br>
2.2 Click start.

![](/source/docs/images/install-uninstall-docker/02-Ubu-VM-Start.PNG)
  

3: Open terminal
 

![](/source/docs/images/install-uninstall-docker/03-Open-Terminal.PNG)
  
4: Run the following command to update package management:

> sudo apt update

![](/source/docs/images/install-uninstall-docker/04-apt-update.PNG)

5: Run the following command to Install docker :

> sudo apt install docker.io

![](/source/docs/images/install-uninstall-docker/05-Install-docker.PNG)

6: You can also remove docker by running the following command:

> sudo apt remove docker.io

![](/source/docs/images/install-uninstall-docker/06-Remove-docker.PNG)

7: Let's download docker again.

> sudo apt install docker.io

![](/source/docs/images/install-uninstall-docker/07-Install-docker.PNG)

8: Examine Docker Hub:

8.1.1 Navigate to [Docker Hub](http://hub.docker.com/)</br>
8.1.2 Examine the available images:

![](/source/docs/images/install-uninstall-docker/08-Navigate-explore.PNG)


8.2 We will examine 2 examples which are: </br>
8.2.1: Ubuntu . </br>
8.2.2: Alpine. </br>

![](/source/docs/images/install-uninstall-docker/09-Ubuntu-Apline.PNG)

8.3 Navigate to ubuntu you can see the pull command highlighted:
</br></br>
![](/source/docs/images/install-uninstall-docker/010-Ubuntu.PNG)
- Let's Pull some images:
</br></br>

9: Open terminal window.</br>
9.1 Run the following command to pull ubuntu and by using the standalone command it will always pull the latest image for any image you're trying to pull:</br>
9.2 Run the following command to check the images we have inside.

> sudo docker pull ubuntu

> sudo docker images

![](/source/docs/images/install-uninstall-docker/011-pull-ubuntu.PNG)

10: Navigate back to alpine page you can see the alpine command.

![](/source/docs/images/install-uninstall-docker/012-alpine.PNG)

11.1 Run the following command to pull alpine latest image: </br>
11.2 Run the following command to check the docker images, you can notice we have 2 images (ubuntu that we have pulled earlier and alpine).

> sudo docker pull alpine

> sudo docker images

![](/source/docs/images/install-uninstall-docker/013-pull-alpine.PNG)


12.1 But we can pull an image with a specific version it's the same command followed by :then the version you want to pull like the following command for alpine. </br>
12.2 Run docker images to check all the images, you can notice 3 images 2 with the latest tag and one with the specific tag 3.12.

> sudo docker pull image alpine:3.12

> sudo docker images

![](/source/docs/images/install-uninstall-docker/014-alpine-tag.PNG)

13: Let's Make our first container by running the following command.

> sudo docker run -it --name con-1 ubuntu /bin/bash

![](/source/docs/images/install-uninstall-docker/015-run-cont.PNG)


14: Let's Open a new Terminal. </br>
14.1 Right-click on the highlighted terminal icon. </br>
14.2 Click new window. </br>

![](/source/docs/images/install-uninstall-docker/016-new-terminal.PNG)

  
15: Run the following command to check the running containers.

> sudo docker ps

![](/source/docs/images/install-uninstall-docker/017-docker-ps.PNG)

16: Navigate to the first terminal window to exit the container:

> exit

![](/source/docs/images/install-uninstall-docker/018-exit-con.PNG)

  

17: Navigate to the second terminal to check running containers again:

> sudo docker ps

![](/source/docs/images/install-uninstall-docker/019-docker-ps.PNG)

  

18: Navigate to the first terminal to create the second container:

> sudo docker run -it --name con-2 ubuntu /bin/bash

![](/source/docs/images/install-uninstall-docker/020-docker-con2.PNG)

  

19: Navigate to the second terminal to list all containers -a (running and not running) also you can notice that it also detect that container with name con-1 exited about a minute ago.

> sudo docker ps -a


![](/source/docs/images/install-uninstall-docker/021-dockeps-all.PNG)

  

20: Navigate to the first terminal and exit the 2nd container.

> exit

![](/source/docs/images/install-uninstall-docker/022-exit.PNG)

21: Navigate to the second terminal to check running containers.

> sudo docker ps

![](/source/docs/images/install-uninstall-docker/023-dockerps.PNG)