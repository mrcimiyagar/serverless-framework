FROM node:10
WORKDIR /ateyetekoon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
