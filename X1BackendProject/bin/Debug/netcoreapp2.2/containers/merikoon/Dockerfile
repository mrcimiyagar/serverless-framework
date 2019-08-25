FROM node:10
WORKDIR /merikoon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
