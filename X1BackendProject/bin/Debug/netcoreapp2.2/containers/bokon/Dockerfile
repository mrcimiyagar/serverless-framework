FROM node:10
WORKDIR /bokon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "bin/www"]
