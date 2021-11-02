from bs4 import BeautifulSoup
from selenium import webdriver
import time



options = webdriver.ChromeOptions()
options.add_argument('headless')

# 환경 변수로 등록한 경우
driver = webdriver.Chrome(options=options)

# 절대 경로로 실행시킬 경우
# driver = webdriver.PhantomJS("web driver.exe 파일 경로")
# driver = webdriver.PhantomJS()

driver.get("https://shop.tesla.com/ko_kr/product/ccs-combo-1-adapter---south-korea")
#  driver.get("https://shop.tesla.com/ko_kr/product/j1772--adapter")
time.sleep(3) # 접속하는 동안 대기

val = driver.find_elements_by_class_name("product__container__details")
# org = driver.find_element_by_id('org')

for title in val:
    print(title.text)

# print( val.text)

# print(driver.page_source) # 해당 페이지 전체 HTML 반환
# product__container__details

page = driver.page_source

soup = BeautifulSoup(page, "html.parser")

title_list = soup.find_all("h3", class_="product__container__details")
for title in title_list:
   print(title.get_text())



# print(driver.execute_script("return document.documentElement.innerHTML;"))



# print(driver.find_element_by_id("content").text) # bs4 기능 일부 지원
driver.close()