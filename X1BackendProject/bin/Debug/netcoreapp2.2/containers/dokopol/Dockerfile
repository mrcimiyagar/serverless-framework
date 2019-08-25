FROM node:10
WORKDIR /dokopol
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "bin/www"]
