# Install/uninstall Docker on Ubuntu on HV:

1.1 Navigate to Hyber-V manager -> open ubuntu VM-> login

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=1676317518143056997)

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=7084547680363861256)  
  

  

1.2 Open terminal

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=8431770446365625158)  
  
1.3 Update package management :

  

> sudo apt update

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=372238960124364454)

1.4 Install docker :

  

> sudo apt install [docker.io](http://docker.io/)

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=16055785453372219807)

1.5 To uninstall docker :

  

> sudo apt remove [docker.io](http://docker.io/)

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=2611577060581306314)

  

1.6 Install docker again :

> sudo apt install [docker.io](http://docker.io/)

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=17445331285326286513)

2. Examine Docker Hub:

2.1 Navigate to [Docker Hub](http://hub.docker.com/)

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=16457071663192833827)

  

2.2 Examine the available images:

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=16499530170422652613)

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=12539748858641408587)

3. Pull some images:

3.1 Open terminal window:

3.2

> sudo docker pull ubuntu

  

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=241825733576312125)

3.3

> sudo docker images

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=12736233665374948410)

  

3.4

> sudo docker pull alpine

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=4928597868810940426)

  

3.5

> sudo docker images

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=9350968220880789199)

  

3.6: Pull an image with a specific version

> sudo docker pull image alpine:3.12

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=3860966525346312314)

  

3.7

> sudo docker images

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=5219988617531109675)

3.8: Making a new ubuntu container

> sudo docker run -it --name con-1 ubuntu /bin/bash

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=7150152645777898814)

  

3.9 : Open another terminal window

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=10033183888239211828)

  

3.10 Checking running containers:

> sudo docker ps

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=11844653406067881569)

  

3.11 Navigate to the first terminal window to exit the container:

> exit

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=4445139425493838704)

  

3.12 Navigate to the second terminal to check running containers again:

> sudo docker ps

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=8949753795034155859)

  

3.13 Navigate to the first terminal to create the second container:

> sudo docker run -it --name con-2 ubuntu /bin/bash

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=10240168802039473495)

  

3.14 Navigate to the second terminal to list all containers -a (running and not running)

> sudo docker ps -a

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=1330730573290104002)

  

3.15 Navigate to the first terminal.

> exit

  

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=16723102997873528731)

3.16 Navigate to the second terminal to check running containers:

> sudo docker ps

  

![](http://devopsvisionsqa.mohamedradwan.com/?qa=blob&qa_blobid=3141439925248690541)