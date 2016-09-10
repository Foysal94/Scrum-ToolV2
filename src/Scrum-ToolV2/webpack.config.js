/* eslint-disable no-undef */
var path = require('path');
var glob = require("glob");
var webpack = require('webpack');
var autoprefixer = require('autoprefixer');
var postcssSimpleVars = require("postcss-simple-vars")
var postcssNested = require("postcss-nested")
var postcssImport = require("postcss-import")
var postcssMixins = require('postcss-mixins')
var stylelint = require('stylelint')
var CleanWebpackPlugin = require('clean-webpack-plugin');

var PATHS = {
   app:  glob.sync("./assets/ES2015/*.js"),
   build: path.join(__dirname, 'wwwroot')
};


var config = {
   entry: {
      app: PATHS.app
   },

   output: {
      path: PATHS.build,
      filename: '[name].bundle.js',
   },

   resolve: {
      modulesDirectories: ['node_modules', 'assets', 'src'],
      extension: [' ', '.js', '.css']
   },

   module: {

      preLoaders: [
         {
            test: /(scss|css)$/,
            loaders: ['postcss'],
         },
         /*
         {
             test: /\.jsx?$/,
             loaders: ['eslint'],
             include: PATHS.app
         }
         */
      ],

      loaders: [
         {
            test: /\.jsx?$/,
            exclude: /node_modules/,
            loader: "babel-loader"
         },
         {
            test: /(scss|css)$/,
            loaders: [
               'style',
               'css-loader?modules=true&sourceMap=true&localIdentName=[name]__[local]___[hash:base64:5]',
               'postcss'

            ],
         },
         {
            test   : /\.(ttf|eot|svg|woff(2)?)(\?[a-z0-9=&.]+)?$/,
            loader : 'file-loader?name=fonts/[name].[hash].[ext]',
         }
      ]

   },

   postcss: function(paths, webpack)  {

      return [
         autoprefixer({
            browsers: ['last 2 versions']
         }),

         postcssImport ({
            path: paths,
            addDependencyTo: webpack
         }),
			postcssMixins,
         postcssSimpleVars,
         postcssNested
      ]

   },
	
	plugins: [
		new CleanWebpackPlugin(
            [
                './wwwroot/js/',
                './wwwroot/css/'
            ]
      ),
      // Output extracted CSS to a file
      new ExtractTextPlugin('[name].css', {
         allChunks: true
      })
	]



};

module.exports = config;

/* eslint-enable no-undef */