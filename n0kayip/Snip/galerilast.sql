/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 100124
 Source Host           : localhost:3306
 Source Schema         : galeri

 Target Server Type    : MySQL
 Target Server Version : 100124
 File Encoding         : 65001

 Date: 17/07/2017 16:12:52
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for resimler
-- ----------------------------
DROP TABLE IF EXISTS `resimler`;
CREATE TABLE `resimler`  (
  `resim_id` int(11) NOT NULL AUTO_INCREMENT,
  `resim_adi` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `resim_baslik` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  `resim_parargraf` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  PRIMARY KEY (`resim_id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 21 CHARACTER SET = utf8 COLLATE = utf8_turkish_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Table structure for ziyaretciler
-- ----------------------------
DROP TABLE IF EXISTS `ziyaretciler`;
CREATE TABLE `ziyaretciler`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `ziyaretci` text CHARACTER SET utf8 COLLATE utf8_turkish_ci,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_turkish_ci ROW_FORMAT = Compact;

SET FOREIGN_KEY_CHECKS = 1;
