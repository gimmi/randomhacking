FROM node:12-alpine3.12

# https://github.com/nodejs/docker-node/blob/master/docs/BestPractices.md#handling-kernel-signals
RUN apk add --no-cache tini

ENV NODE_ENV=production

WORKDIR /app

COPY package*.json src/ ./

RUN npm install

ENTRYPOINT ["/sbin/tini", "--", "node", "index.js"]
