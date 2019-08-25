FROM node:10
WORKDIR /atefehkos
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
