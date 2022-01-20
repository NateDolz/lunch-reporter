FROM node
WORKDIR /usr/src

COPY . .

RUN npm install

EXPOSE 80

RUN npm run build


CMD ["node", "./dist/bot.js"]