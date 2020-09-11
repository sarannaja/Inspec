var express = require("express");
var router = express.Router();
const nodeCmd = require("node-cmd");
/* GET home page. */
router.post("/", function (req, res, next) {
  let command = req.body.command
  nodeCmd.get(command,(err, data, stderr) => res.json(data)
    // res.render("index", { title: data.toString() })
  );
});

module.exports = router;
