FROM node:10
WORKDIR /atkos
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
