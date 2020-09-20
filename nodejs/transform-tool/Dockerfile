FROM node:lts-alpine

ENV NODE_ENV=production

WORKDIR /app

COPY package*.json src/ ./

RUN npm install

ENTRYPOINT ["node", "index.js"]
