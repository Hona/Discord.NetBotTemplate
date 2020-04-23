echo '-= Stopping template-bot-production Container =-'
docker container stop template-bot-production

echo '-= Removing Old template-bot-production Container =-'
docker container rm template-bot-production

echo '-= Building Docker Image from Dockerfile ='
docker build -t template-bot -f ./src/DiscordBotTemplate/Dockerfile .

echo '-= Runnning the Image ='
docker run -e "ENVIRONMENT=DEVELOPMENT" -v $PWD/PRODUCTION:/app/PRODUCTION -v $PWD/DEVELOPMENT:/app/DEVELOPMENT --network host --restart on-failure:5 --name "template-bot-production" -d template-bot