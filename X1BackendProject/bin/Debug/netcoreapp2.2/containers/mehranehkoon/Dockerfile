FROM node:10
WORKDIR /mehranehkoon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
