FROM node:alpine3.11

WORKDIR /app

COPY server/ ./

ENV NODE_ENV production

RUN npm install

ENTRYPOINT ["node", "app.js"]
