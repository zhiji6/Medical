using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SY.Com.Medical.Attribute;
using SY.Com.Medical.BLL;
using SY.Com.Medical.BLL.Clinic;
using SY.Com.Medical.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SY.Com.Medical.Extension;

namespace SY.Com.Medical.WebApi.Controllers.Clinic
{
    /// <summary>
    /// 物料控制器(药品、项目、材料)
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Api_Tenant]
    public class GoodController : ControllerBase
    {
        Good bll = new Good();

        /// <summary>
        /// 获取药品列表-分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<GoodModels>> gets(GoodRequest request )
        {
            BaseResponse<List<GoodModels>> result = new BaseResponse<List<GoodModels>>();
            try {
                if(request.GoodType == 0)
                {
                    request.GoodType = 6;
                }
                var tuple = bll.getGoods(request.TenantId,request.PageSize,request.PageIndex,request.GoodType,request.GoodSort,request.SearchKey);
                if(tuple.Item2 == 0)
                {
                    return new BaseResponse<List<GoodModels>>();
                }
                result.Data = tuple.Item1.ToList().Mapping<GoodModels>();
                result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
                return result;
            }catch(Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取药品详情
        /// </summary>
        /// <param name="request">包含tenantid和goodid</param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<GoodBllModel> get(GoodOneRequest request)
        {            
            BaseResponse<GoodBllModel> result = new BaseResponse<GoodBllModel>();
            try
            {
                result.Data = bll.getGood(request.TenantId, request.GoodId);
                if(result.Data == null)
                {
                    return new BaseResponse<GoodBllModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 添加药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> add(GoodAdd request)
        {
            BaseResponse<int> result = new BaseResponse<int>();
            try
            {
                result.Data = bll.add(request);
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> update(GoodUpdate request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                bll.update(request);
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除药品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> delete(GoodDelete request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                bll.delete(request);
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取药品及项目材料分类字典
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<DicKeyValueModel>> getDrugSortDic(GoodSortModelRequest request)
        {
            BaseResponse<List<DicKeyValueModel>> result = new BaseResponse<List<DicKeyValueModel>>();
            result.Data = bll.getDrugSort(request);
            return result;
        }

        /// <summary>
        /// 获取厂家字典
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<DicKeyValueModel>> getFactory(BaseModel mod)
        {
            BaseResponse<List<DicKeyValueModel>> result = new BaseResponse<List<DicKeyValueModel>>();
            result.Data = bll.getFactory(mod);
            return result;
        }

        /// <summary>
        /// 获取用法字典
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<DicKeyValueModel>> getUsage(BaseModel mod)
        {
            BaseResponse<List<DicKeyValueModel>> result = new BaseResponse<List<DicKeyValueModel>>();
            result.Data = bll.getUsage(mod);
            return result;
        }

        /// <summary>
        /// 获取一天的用量字典
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<DicKeyValueModel>> getEveryDay(BaseModel mod)
        {
            BaseResponse<List<DicKeyValueModel>> result = new BaseResponse<List<DicKeyValueModel>>();
            result.Data = bll.getEveryDay(mod);
            return result;
        }

        /// <summary>
        /// 获取单位字典
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<DicKeyValueModel>> getUnit(BaseModel mod)
        {
            BaseResponse<List<DicKeyValueModel>> result = new BaseResponse<List<DicKeyValueModel>>();
            result.Data = bll.getUnit(mod);
            return result;
        }

        /// <summary>
        /// 新增字典
        /// ex:{ "DicType":1,DicSubType:1,"消炎类"  },表示添加一个西药的药品分类“消炎类”
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> addDic(GoodDicAddRequest request)
        {
            BaseResponse<int> result = new BaseResponse<int>();
            string keyFirst = "";
            string keySecond = "";
            switch (request.DicType)
            {
                case 1:
                    keyFirst = "GoodSort";
                    switch(request.DicSubType)
                    {
                        case 1: keySecond = "西药";break;
                        case 2: keySecond = "中成药";break;
                        case 3: keySecond = "中药";break;
                        default: throw new MyException("输入有误请查看文档");
                    }
                    break;
                case 2: keyFirst = "Factory"; break;
                case 3: keyFirst = "Usage"; break;
                case 4: keyFirst = "EveryDay"; break;
                case 5: keyFirst = "Unit"; break;
                case 6: keyFirst = "GoodSort";keySecond = "项目"; break;
                case 7: keyFirst = "GoodSort";keySecond = "材料"; break;
                default:throw new MyException("输入有误请查看文档"); 
            }
            result.Data = bll.addDic(request.TenantId, request.Value, keyFirst, keySecond);
            return result;
        }

        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> deleteDic(GoodDicDeleteRequest request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                var isdelete = bll.Delete(request.DicId);
                if(isdelete > 0)
                {
                    result.Data = true;
                }
                else
                {
                    result.Data = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> updateDic(GoodDicUpdateRequest request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                var isdelete = bll.Update(request.DicId,request.DicValue);
                if (isdelete > 0)
                {
                    result.Data = true;
                }
                else
                {
                    result.Data = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        

        /// <summary>
        /// 获取项目列表分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<GoodItemModel>> getItems(GoodItemsRequest request)
        {
            BaseResponse<List<GoodItemModel>> result = new BaseResponse<List<GoodItemModel>>();
            try
            {                
                var tuple = bll.getGoods(request.TenantId,request.PageSize,request.PageIndex,4,request.GoodSort,request.SearchKey);
                if(tuple.Item2 == 0 || tuple.Item1 == null)
                {
                    return new BaseResponse<List<GoodItemModel>>();
                }
                result.Data = tuple.Item1.ToList().Mapping<GoodItemModel>();
                result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<GoodItemOneModel> getItem(GoodOneRequest request)
        {
            BaseResponse<GoodItemOneModel> result = new BaseResponse<GoodItemOneModel>();
            try
            {
                result.Data = bll.getGood(request.TenantId,request.GoodId).Mapping<GoodItemOneModel>();
                if(result.Data == null)
                {
                    return new BaseResponse<GoodItemOneModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> addItem(GoodItemAdd request)
        {
            BaseResponse<int> result = new BaseResponse<int>();
            try
            {
                request.GoodType = 4;
                result.Data = bll.add(request.Mapping<GoodAdd>());
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> updateItem(GoodItemUpdate request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                request.GoodType = 4;
                bll.update(request.Mapping<GoodUpdate>());
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> deleteItem(GoodDelete request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                bll.delete(request);
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取材料分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<GoodMaterialModels>> getsMaterial(GoodMaterialRequest request)
        {
            BaseResponse<List<GoodMaterialModels>> result = new BaseResponse<List<GoodMaterialModels>>();
            try
            {
                request.GoodType = 5;
                var tuple = bll.getGoods(request.TenantId,request.PageSize,request.PageIndex,request.GoodType,request.GoodSort,request.SearchKey);
                if(tuple.Item2 == 0 || tuple.Item1 == null)
                {
                    return new BaseResponse<List<GoodMaterialModels>>();
                }
                result.Data = tuple.Item1.ToList().Mapping<GoodMaterialModels>();
                result.CalcPage(tuple.Item2, request.PageIndex, request.PageSize);
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 获取材料详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<GoodMaterialModel> getMaterial(GoodOneRequest request)
        {
            BaseResponse<GoodMaterialModel> result = new BaseResponse<GoodMaterialModel>();
            try
            {
                result.Data = bll.getGood(request.TenantId,request.GoodId).Mapping<GoodMaterialModel>();
                if(result.Data == null)
                {
                    return new BaseResponse<GoodMaterialModel>();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 添加材料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<int> addMaterial(GoodMaterialEditModel request)
        {
            BaseResponse<int> result = new BaseResponse<int>();
            try
            {
                var goodadd = request.Mapping<GoodAdd>();
                goodadd.GoodType = 5;
                result.Data = bll.add(goodadd);
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改材料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> updateMaterial(GoodMaterialEditModel request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                var goodupdate = request.Mapping<GoodUpdate>();
                goodupdate.GoodType = 5;
                bll.update(goodupdate);
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 删除材料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<bool> deleteMaterial(GoodDelete request)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            try
            {
                bll.delete(request);
                result.Data = true;
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }

        /// <summary>
        /// 下载物料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<string> getDownloadUrl(GoodDownFileType request)
        {
            BaseResponse<string> result = new BaseResponse<string>();
            try
            {
                switch(request.Type)
                {
                    case 1: result.Data = "http://" + Request.Host.Value + "/GoodTemplate/药品模板.xlsx";break;
                    case 2: result.Data = "http://" + Request.Host.Value + "/GoodTemplate/项目模板.xlsx"; break;
                    case 3: result.Data = "http://" + Request.Host.Value + "/GoodTemplate/材料模板.xlsx"; break;
                    default: result.Data = "http://" + Request.Host.Value + "/GoodTemplate/药品项目模板.xlsx"; break;
                }
                return result;
            }
            catch (Exception ex)
            {
                if (ex is MyException)
                {
                    return result.busExceptino(Enum.ErrorCode.业务逻辑错误, ex.Message);
                }
                else
                {
                    return result.sysException(ex.Message);
                }
            }
        }
    }
}
