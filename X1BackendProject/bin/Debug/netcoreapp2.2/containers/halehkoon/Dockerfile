FROM node:10
WORKDIR /halehkoon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
