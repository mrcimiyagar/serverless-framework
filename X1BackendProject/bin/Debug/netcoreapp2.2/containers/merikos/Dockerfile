FROM node:10
WORKDIR /merikos
COPY package*.json ./
RUN npm install
COPY . .
CMD ["node", "app"]
