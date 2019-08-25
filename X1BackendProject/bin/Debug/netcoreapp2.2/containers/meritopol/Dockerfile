FROM node:10
WORKDIR /meritopol
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "bin/www"]
