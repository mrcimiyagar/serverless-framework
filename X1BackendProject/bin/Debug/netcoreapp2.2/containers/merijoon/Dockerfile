FROM node:10
WORKDIR /merijoon
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
