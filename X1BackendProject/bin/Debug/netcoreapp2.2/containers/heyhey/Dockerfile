FROM node:10
WORKDIR /heyhey
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app.js"]
