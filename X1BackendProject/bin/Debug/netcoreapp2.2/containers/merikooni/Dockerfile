FROM node:10
WORKDIR /merikooni
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "bin/www"]
