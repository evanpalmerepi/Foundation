@echo off 
cls 
echo ###################################################################### 
echo #           Rebuid the current application from default              # 
echo ###################################################################### 
echo #                                                                    # 
echo #       NOTE: This will **DROP** the existing DB                     # 
echo #             and resetup so use with caution!!                      # 
echo #                                                                    # 
echo #       Crtl+C NOW if you are unsure!                                # 
echo #                                                                    # 
echo ###################################################################### 
pause 
setup GreaterBank greaterbank.episerverau.com greaterbankcm.episerverau.com "" PresalesEP -U demo -P demo 
