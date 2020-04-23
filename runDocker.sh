echo '-= Stopping template-bot-production Container =-'
docker container stop template-bot-production

echo '-= Removing Old template-bot-production Container =-'
docker container rm template-bot-production

echo '-= Building Docker Image from Dockerfile ='
docker build -t template-bot -f ./src/TemplateDiscordBot/Dockerfile .

echo '-= Runnning the Image ='
docker run -v $PWD/PRODUCTION:/app/PRODUCTION -v $PWD/DEVELOPMENT:/app/DEVELOPMENT --network host --restart on-failure:5 --name "template-bot-production" -d template-bot