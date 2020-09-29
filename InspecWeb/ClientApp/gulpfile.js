const { src, dest } = require('gulp');
var replace = require('gulp-replace')

exports.default = function (cb) {    
    src(['dist/index.html'])
        .pipe(replace('type="module"', 'type="text/javascript"'))
        .pipe(dest('dist/'));
    cb();
};