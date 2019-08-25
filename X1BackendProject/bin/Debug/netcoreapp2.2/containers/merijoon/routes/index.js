var express = require('express');
var router = express.Router();
let mongoose = require('mongoose');

router.get('/', function(req, res, next) {
  mongoose
      .connect(
          'mongodb://aseman:3x2fG1b65sg4hN68sr4yj8j6k5Bstul4yi56l453tsK5346u5s4R648j@' + process.env.MONGODB_HOST + ':27017/admin',
          { useNewUrlParser: true }
      )
      .then(() => console.log('MongoDB Connected'))
      .catch(err => console.log(err));


// DB schema
  const ItemSchema = new mongoose.Schema({
    name: {
      type: String,
      required: true
    },
    date: {
      type: Date,
      default: Date.now
    }
  });
  Item = mongoose.model('item', ItemSchema);
  const newItem = new Item({
    name: 'keyhan'
  });
  newItem.save();
  res.send('thanks honey');
});

module.exports = router;
