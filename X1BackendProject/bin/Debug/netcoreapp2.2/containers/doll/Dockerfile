FROM node:10
WORKDIR /doll
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "bin/www"]
